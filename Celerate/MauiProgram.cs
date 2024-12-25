using DevExpress.Maui;
using DevExpress.Maui.Core;
using DevExpress.Charts.Native;

namespace Celerate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThemeManager.ApplyThemeToSystemBars = true;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress(useLocalization: true)
                .UseDevExpressGauges()
                .UseDevExpressControls()
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressEditors()
                .UseDevExpressDataGrid()
                .UseDevExpressScheduler()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                    fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("Quicksand-Bold.ttf", "FontBold");
                    fonts.AddFont("Quicksand-Light.ttf", "FontLight");
                    fonts.AddFont("Quicksand-Medium.ttf", "FontMed");
                    fonts.AddFont("Quicksand-Regular.ttf", "FontRegular");
                    fonts.AddFont("Quicksand-SemiBold.ttf", "FontSemiBold");
                });
            DevExpress.Maui.Charts.Initializer.Init();
            DevExpress.Maui.CollectionView.Initializer.Init();
            DevExpress.Maui.Controls.Initializer.Init();
            DevExpress.Maui.Editors.Initializer.Init();
            DevExpress.Maui.DataGrid.Initializer.Init();
            DevExpress.Maui.Scheduler.Initializer.Init();
            return builder.Build();
        }
    }
}