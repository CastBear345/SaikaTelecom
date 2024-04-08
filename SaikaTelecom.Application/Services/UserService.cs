namespace SaikaTelecom.Application.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;
    private long CurrentUserId => long.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public UserService(ApplicationDbContext dbContext, IHttpContextAccessor accessor,
        IMapper mapper, PasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _httpContext = accessor.HttpContext ?? throw new ArgumentException(nameof(accessor.HttpContext));
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Sign in a user with the provided credentials.
    /// </summary>
    /// <param name="dto">Sign-in user DTO containing email and password.</param>
    /// <returns>A base result containing user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> SignInAsync(SignInUserDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) return new BaseResult<UserResponse>() { ErrorMessage = "Invalid email." };

        if (user.BlockingDate != null) return new BaseResult<UserResponse>() { ErrorMessage = "User is blocked." };

        if (string.IsNullOrEmpty(dto.Password)) return new BaseResult<UserResponse>() { ErrorMessage = "Enter password." };

        if (_passwordHasher.Verify(dto.Password, user.PasswordHash)) {
            var userGetDto = _mapper.Map<UserGetDto>(user);
            await SignInWithHttpContext(userGetDto);
            return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
        }

        return new BaseResult<UserResponse>() { ErrorMessage = "Invalid password." };
    }

    /// <summary>
    /// Register a new user with the provided information.
    /// </summary>
    /// <param name="dto">Sign-up user DTO containing user details.</param>
    /// <returns>A base result containing user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> SignUpAsync(SignUpUserDto dto)
    {
        var existingUser = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existingUser != null) return new BaseResult<UserResponse>() { ErrorMessage = "User with this email already exists." };

        if (string.IsNullOrEmpty(dto.Password)) return new BaseResult<UserResponse>() { ErrorMessage = "Invalid password." };

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _passwordHasher.Generate(dto.Password);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return new BaseResult<UserResponse> () { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Retrieve all users.
    /// </summary>
    /// <returns>A base result containing a list of user responses or an error message.</returns>
    public async Task<BaseResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
        if (users == null) return new BaseResult<List<UserResponse>>() { ErrorMessage = "Users not found." };

        return new BaseResult<List<UserResponse>> { Data = _mapper.Map<List<UserResponse>>(users) };
    }

    /// <summary>
    /// Get the currently logged-in user.
    /// </summary>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> GetCurrentUserAsync()
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == CurrentUserId);
        if (user == null) return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };

        return new BaseResult<UserResponse> { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Block a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to block.</param>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> BlockUserAsync(long userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) 
            return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };
        else if(user.Id == CurrentUserId)
            return new BaseResult<UserResponse>() { ErrorMessage = "Can't block the current user." };

        user.BlockingDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Unblock a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to unblock.</param>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> UnblockUserAsync(long userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) 
            return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };
        else if(user.BlockingDate == null)
            return new BaseResult<UserResponse>() { ErrorMessage = "User not blocked." };

        user.BlockingDate = null;
        await _dbContext.SaveChangesAsync();
        return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Delete a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> DeleteUserAsync(long userId)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };
        else if (user.Id == CurrentUserId)
            return new BaseResult<UserResponse>() { ErrorMessage = "You can't delete yourself." };

        if (user.Role != Roles.Admin || _httpContext.User.IsInRole(nameof(Roles.Owner))) {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
        }

        return new BaseResult<UserResponse>() { ErrorMessage = "You can't remove the admin." };
    }

    /// <summary>
    /// Change the role of a user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to change the role.</param>
    /// <param name="newRole">The new role for the user.</param>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> ChangeUserRoleAsync(long userId, Roles newRole)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) 
            return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };
        else if (user.Role == newRole)
            return new BaseResult<UserResponse>() { ErrorMessage = "The user already has this role." };

        user.Role = newRole;
        await _dbContext.SaveChangesAsync();
        return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Change the password of the currently logged-in user.
    /// </summary>
    /// <param name="dto">DTO containing old and new password.</param>
    /// <returns>A base result containing the user response or an error message.</returns>
    public async Task<BaseResult<UserResponse>> ChangePasswordCurrentlyUserAsync(ChangeUserPasswordDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == CurrentUserId);
        if (user == null)
            return new BaseResult<UserResponse>() { ErrorMessage = "User not found." };
        else if (dto.OldPassword.IsNullOrEmpty())
            return new BaseResult<UserResponse>() { ErrorMessage = "Old password entered incorrectly." };
        else if (dto.NewPassword.IsNullOrEmpty())
            return new BaseResult<UserResponse>() { ErrorMessage = "New password entered incorrectly." };

        user.PasswordHash = _passwordHasher.Generate(dto.NewPassword);
        await _dbContext.SaveChangesAsync();
        return new BaseResult<UserResponse>() { Data = _mapper.Map<UserResponse>(user) };
    }

    /// <summary>
    /// Log out the currently logged-in user.
    /// </summary>
    /// <returns>A base result indicating success or an error message.</returns>
    public async Task<BaseResult<UserResponse>> LogOutAsync()
    {
        await _httpContext.SignOutAsync();
        return new BaseResult<UserResponse>();
    }

    /// <summary>
    /// Sign in a user with the provided user DTO.
    /// </summary>
    /// <param name="userDto">The user DTO to sign in.</param>
    /// <returns>A task representing the sign-in operation.</returns>
    private Task SignInWithHttpContext(UserGetDto userDto)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.Email, userDto.Email),
            new(ClaimTypes.Role, userDto.Role.ToString()),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "cookie");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        return _httpContext.SignInAsync(claimsPrincipal);
    }
}
