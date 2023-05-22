namespace InventoryServer.Domain.Entities
{
	/// <summary>
	/// Авторизированный пользователь
	/// </summary>
	public class User
	{
		public int Id { get; set; }

		/// <summary>
		/// Логин
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		/// Роль на сервере \ Права на сервере
		/// </summary>
		public UserRole UserRole { get; set; }
	}
}