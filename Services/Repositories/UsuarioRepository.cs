namespace Services.Repositories
{
    public interface UsuarioRepository<T> where T : class
    {
        Task<T> FindAUser(string nickName);
        Task<Boolean> CreateUSuario(T usuario);
        Task<Boolean> UdpateUsuario(T usuario);
        Task<Boolean> DeleteUsuario(int idUsuario);
        Task<List<String>> ListUserByRoom(int RoomFilter);
        Task<T> FindUserById(int usuarioId);
    }
}
