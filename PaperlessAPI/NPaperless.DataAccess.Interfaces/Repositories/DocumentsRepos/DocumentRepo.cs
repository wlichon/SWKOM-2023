using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public DocumentRepo(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Document> CreateOneDoc(Document document)
        {

            try
            {
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                uint id = document.id;
            }
            catch (DbUpdateException ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return null;
            }
            return document;

        }

        public async Task<Document> DeleteOneDoc(uint id)
        {
            Document? doc = new Document();

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
                _logger.Log(LogLevel.Error, ex.Message);
                return null;
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
                _logger.Log(LogLevel.Error, ex.Message);
                return null;
            }

            return docs; 
        }

        public async Task<Document> GetOneDoc(uint id)
        {
            Document? doc = new Document();

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
                _logger.Log(LogLevel.Error, ex.Message);
                return null;
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

            doc.correspondent = document.correspondent;
            doc.documentType = document.documentType;
            doc.tags = document.tags;
            doc.title = document.title;
            doc.created = document.created;
            
            doc.created_date = TimeZoneInfo.ConvertTimeToUtc(document.created_date);

            try
            {
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return null;
            }

            return doc;

        }
    }
}
