using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPaperless.Services.Data;
using NPaperless.Services.Models;
using NPaperless.Services.Services.CorrespondentsRepo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CorrespondentRepo : ICorrespondentRepo
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public CorrespondentRepo(DataContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Correspondent> CreateOne(Correspondent correspondent)
    {
        var corr = _mapper.Map<Correspondent>(correspondent);
        _context.Correspondents.Add(corr);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }
        return corr;

    }

    public async Task<List<Correspondent>> GetAll()
    {
        List<Correspondent> corrs = null; 

        try
        {
            corrs = await _context.Correspondents.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }

        return corrs ?? new List<Correspondent>();

    }

    public async Task<Correspondent> UpdateOne(long id, Correspondent correspondent)
    {
        var corr = await _context.Correspondents.FindAsync(id);
        if(corr == null)
        {
            return null;
        }

        corr.match = correspondent.match;
        corr.last_correspondence = correspondent.last_correspondence;
        corr.matching_algorithm = correspondent.matching_algorithm;
        corr.name = correspondent.name;
        corr.is_insensitive = correspondent.is_insensitive;
        corr.document_count = correspondent.document_count;
        corr.last_correspondence = corr.last_correspondence;

        try
        {
            await _context.SaveChangesAsync();

        }
        catch(DbUpdateException ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }

        return corr;    
        
    }

    public async Task<Correspondent> DeleteOne(long id)
    {
        Correspondent correspondent = new Correspondent();

        try
        {
            correspondent = await _context.Correspondents.FindAsync(id);

            if (correspondent == null)
            {
                // Correspondent with the given id was not found
                return correspondent;
            }

            _context.Correspondents.Remove(correspondent);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message);
        }

        return correspondent;
    }
}
