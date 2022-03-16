using Models;

namespace Api.Server.Services;

public interface IUserService
{
    /// <summary>
    /// Lấy tất cả user
    /// </summary>
    /// <returns></returns>
    Task<List<UserModel>> GetAllUser();
    /// <summary>
    /// Lấy thông tin user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="passwd"></param>
    /// <returns></returns>
    Task<UserModel> GetUser(string username,string passwd);
    /// <summary>
    /// Sửa thông tin user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task UpdateUser(UserModel user);
    /// <summary>
    /// Xóa tài khoản
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task DeleteUser(string userName);
    /// <summary>
    /// Tạo tài khoản
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task AddUser(UserModel user);
}