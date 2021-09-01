using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor.Services;
using System.Globalization;
using Microsoft.AspNetCore.Components.Authorization;
using ClenoviSDSM.Client.Auth;
using ClenoviSDSM.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ClenoviSDSM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            CultureInfo mkCulture = CultureInfo.CreateSpecificCulture("bg-BG");

            DateTimeFormatInfo formatInfo = mkCulture.DateTimeFormat;
            formatInfo.FirstDayOfWeek = DayOfWeek.Monday;
            formatInfo.AbbreviatedDayNames = new[] { "не", "по", "вт", "ср", "че", "пе", "са" };
            formatInfo.DayNames = new[] { "недела", "понеделник", "вторник", "среда", "четврток", "петок", "сабота" };
            var monthNames = new[]
            {
            "јануари","февруари","март","април","мај","јуни",
            "јули","август","септември","октомври","ноември","декември",
            ""
        };

            formatInfo.AbbreviatedMonthNames =
        formatInfo.MonthNames =
            formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;

            formatInfo.ShortDatePattern = "dd.MM.yyyy";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.YearMonthPattern = "MMMM yyyy";
            mkCulture.DateTimeFormat = formatInfo;

            CultureInfo.DefaultThreadCurrentCulture = mkCulture;
            CultureInfo.DefaultThreadCurrentUICulture = mkCulture;
            builder.Services.AddAuthorizationCore();  

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
          
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IAcountService, AccountService>();
            builder.Services.AddScoped<ITokenManagerService, TokenManagerService>();
            builder.Services.AddScoped<IExportExcelService, ExportExcelService>();

            await builder.Build().RunAsync();
        }
    }
}
