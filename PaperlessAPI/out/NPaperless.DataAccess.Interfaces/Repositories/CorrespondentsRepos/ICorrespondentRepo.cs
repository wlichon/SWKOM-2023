using NPaperless.Services.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPaperless.Services.Services.CorrespondentsRepo
{
    public interface ICorrespondentRepo
    {
        Task<Correspondent> CreateOne(Correspondent correspondent);
        Task<List<Correspondent>> GetAll();

        Task<Correspondent> UpdateOne(long id, Correspondent correspondent);

        Task<Correspondent> DeleteOne(long id);

    }
}
