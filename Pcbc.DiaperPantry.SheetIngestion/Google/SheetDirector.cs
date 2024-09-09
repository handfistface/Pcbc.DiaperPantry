namespace Pcbc.DiaperPantry.SheetIngestion.Google
{
    public interface ISheetDirector
    {
        void PullData();
    }

    public class SheetDirector : ISheetDirector
    {
        private readonly IGoogleSheetService _googleSheetService;

        public SheetDirector(IGoogleSheetService googleSheetService)
        {
            _googleSheetService = googleSheetService;
        }

        public void PullData()
        {
            _googleSheetService.Authenticate();
            var data = _googleSheetService.GetSheet();
        }
    }
}
