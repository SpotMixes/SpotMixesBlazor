using System.Collections.Generic;
using System.Threading.Tasks;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using SpotMixesBlazor.Shared.ModelsData;

namespace SpotMixesBlazor.Server.Services
{
    public class PaymentService
    {
        public async Task<string> GeneratePreference(PreferenceData preferenceData)
        {
            MercadoPagoConfig.AccessToken = "TEST-4611383673556464-112516-d60802c7350bd19f431ed45e5238fd6e-572771438";

            var preferenceRequest = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new()
                    {
                        Title = preferenceData.Title,
                        CurrencyId = preferenceData.CurrencyId,
                        PictureUrl = preferenceData.PictureUrl,
                        Description = preferenceData.Description,
                        Quantity = preferenceData.Quantity,
                        UnitPrice = preferenceData.UnitPrice
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://spotmixes.com/pago-exitoso",
                    Failure = "https://spotmixes.com/pago-fallido",
                    Pending = "https://spotmixes.com/pago-pendiente"
                },
                AutoReturn = "approved"
            };

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(preferenceRequest);
            
            return preference.Id;
        }
    }
}