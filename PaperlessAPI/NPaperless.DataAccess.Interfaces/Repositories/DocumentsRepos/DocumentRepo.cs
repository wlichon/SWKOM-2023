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

        public DocumentRepo(DataContext context)
        {
            _context = context;
  
        }

        public async Task<Document> CreateOneDoc(Document document)
        {
            _context.Documents.Add(document);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return document;

        }

        public async Task<Document> DeleteOneDoc(uint id)
        {
            Document doc;

            try
            {
                doc = await _context.Documents.FindAsync(id);

                if (doc == null)
                {
                    return null;
                }

                _context.Documents.Remove(doc);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
                throw; 
            }

            return doc;
        }

        public async Task<List<Document>> GetAllDocs()
        {
            List<Document> docs = null;  

            try
            {
                docs = await _context.Documents.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
              
            }

            return docs ?? new List<Document>(); 
        }

        public async Task<Document> GetOneDoc(uint id)
        {
            Document doc;

            try
            {
                doc = await _context.Documents.FindAsync(id);

                if (doc == null)
                {
                    return null;
                }
       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }

            return doc;
        }

        public async Task<Document> UpdateOneDoc(uint id, Document document)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null)
            {
                return null;
            }

            doc.Correspondent = document.Correspondent;
            doc.DocumentType = document.DocumentType;
            doc.Tags = document.Tags;
            doc.Title = document.Title;
            doc.Created = document.Created;
            await _context.SaveChangesAsync();

            return doc;

        }
    }
}
