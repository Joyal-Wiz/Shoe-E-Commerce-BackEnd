namespace ECommerce.Application.Constants
{
    public static class ApiMessages
    {
        public static class Success
        {
            public const string Login = "Login successful";
            public const string AdminLogin = "Admin login successful";
            public const string Signup = "User registered successfully";
            public const string TokenRefreshed = "Token refreshed successfully";
            public const string UserDetailsFetched = "User details fetched successfully";

        }

        public static class Error
        {
            public const string InvalidCredentials = "Invalid username or password";
            public const string InvalidAdminCredentials = "Invalid admin credentials";
            public const string RefreshTokenInvalid = "Invalid refresh token";
            public const string RefreshTokenExpired = "Refresh token expired";
            public const string UserInactive = "User account is inactive";
            public const string UsernameExists = "Username already exists";
            public const string EmailExists = "Email already exists";
            public const string ServerError = "An unexpected error occurred";
            public const string AdminInactive = "Admin account is inactive";

        }
    }
}
