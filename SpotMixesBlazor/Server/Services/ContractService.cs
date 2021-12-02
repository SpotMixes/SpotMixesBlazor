using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SpotMixesBlazor.Server.DataAccess;
using SpotMixesBlazor.Shared.Models;

namespace SpotMixesBlazor.Server.Services
{
    public class ContractService
    {
        private readonly IMongoCollection<Contract> _contractCollection;

        public ContractService(ISpotMixesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _contractCollection = database.GetCollection<Contract>("Contracts");
        }
        
        public async Task CreateContract(Contract contract)
        {
            await _contractCollection.InsertOneAsync(contract);
        }
        
        public async Task<IReadOnlyList<Contract>> GetContractsByContractedId (string contractedId)
        {
            var contracts = await _contractCollection
                .Find(contract => contract.ContractedId == contractedId)
                .ToListAsync();
            
            return contracts ?? null;   
        }
        
        public async Task<Contract> GetContractsByContractId (string contractId)
        {
            var contract = await _contractCollection
                .Find(contract => contract.Id == contractId)
                .FirstOrDefaultAsync();
            
            return contract ?? null;   
        }
    }
}