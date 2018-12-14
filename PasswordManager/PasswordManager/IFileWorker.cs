using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string filename); 
        Task SaveTextAsync(string filename, string text);  
        Task<string> LoadTextAsync(string filename);  
        Task<IEnumerable<string>> GetFilesAsync(); 
        Task DeleteAsync(string filename); 
    }
}