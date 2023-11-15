using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPaperless.Services.Data;
using NPaperless.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPaperless.Services.Repositories.DocumentsRepos
{
    public class DocumentRepo : IDocumentRepo
    {

        private DataContext _context;
        private readonly IMapper _mapper;

        public DocumentRepo(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Document> CreateOne(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Document> DeleteOne(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Document>> GetAll()
        {
            List<Document> docs = null;  // Initialize to null or an empty list, depending on your preference.

            try
            {
                docs = await _context.Documents.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                // Handle the exception or log it as needed.
                // You might want to throw the exception again if you don't want to suppress it.
            }

            return docs ?? new List<Document>();  // Return the list or an empty list if an exception occurred.
        }

        public Task<Document> GetOne(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Document> UpdateOne(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
