namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public interface IDialogService
	{
		void ShowMessage(string message);   // показ сообщения
		string FilePath { get; set; }   // путь к выбранному файлу
		bool OpenFileDialog();  // открытие файла
		bool SaveFileDialog();  // сохранение файла
	}
}
