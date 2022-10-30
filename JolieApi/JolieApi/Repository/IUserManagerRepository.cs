using BCrypt.Net;
using JolieApi.DataContext;
using JolieApi.Models;
using JolieApi.ViewModels;
using SEENApiV2_Admin.Repository;

namespace JolieApi.Repository
{
    public interface IUserManagerRepository
    {
        List<User> GetUsers();
        UserInfo GetUserInfo(string token);
        string Login(LoginRequest requestBody);
        string Register(RegisterRequest requestBody);
    }

    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly JolieDataContext _context;
        private readonly IJWTManagerRepository _jWtManagerRepository;

        public UserManagerRepository(JolieDataContext context, IJWTManagerRepository jWTManagerRepository)
        {
            this._context = context;
            this._jWtManagerRepository = jWTManagerRepository;
        }

        public List<User> GetUsers()
        {
            return _context.users.Select(s => s).ToList();
        }

        public UserInfo GetUserInfo(string token)
        {
            string email = _jWtManagerRepository.GetEmailFromToken(token);

            var user = _context.users.Select(s => new UserInfo { 
                user_id = s.user_id,
                email = s.email,
                first_name = s.first_name,
                last_name = s.last_name,
                date_of_birth = s.date_of_birth,
                gender = s.gender
            }).Where(w => w.email == email).SingleOrDefault();

            if (user != null)
            {
                return user;
            }
            else
            {
                return new UserInfo();
            }
        }

        public string Login(LoginRequest requestBody)
        {
            var targetUser = _context.users.Where(w => w.email == requestBody.email).SingleOrDefault();

            if (targetUser != null && IsAuthenticated(requestBody.password, targetUser.password))
            {
                return _jWtManagerRepository.GenerateJwtToken(requestBody.email);
            }
            else
            {
                return "";
            }
        }

        public string Register(RegisterRequest requestBody)
        {
            var existedUser = _context.users.SingleOrDefault(s => s.email == requestBody.email);

            if (existedUser == null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestBody.password);
                DateTime currentDate = DateTime.UtcNow;

                var newUser = new User
                {
                    user_id = 0,
                    email = requestBody.email,
                    password = hashedPassword,
                    first_name = requestBody.first_name,
                    last_name = requestBody.last_name,
                    date_of_birth = requestBody.date_of_birth,
                    gender = requestBody.gender,
                    created_at = currentDate,
                    updated_at = currentDate
                };

                _context.Add(newUser);
                _context.SaveChanges();
                return _jWtManagerRepository.GenerateJwtToken(requestBody.email);
            }
            else
            {
                return "existed";
            }
        }

        private static bool IsAuthenticated(string requestPassword, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, userPassword);
        }
    }
}