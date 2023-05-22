namespace InventoryServer.Services.Crypt
{
	public interface ICryptService
	{
		public string ComputeHash(string text);
	}
}