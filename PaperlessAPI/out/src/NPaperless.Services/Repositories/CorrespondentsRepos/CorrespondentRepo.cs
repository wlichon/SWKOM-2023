using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPaperless.Services.Data;
using NPaperless.Services.Migrations;
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
    public CorrespondentRepo(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Correspondent> CreateOne(CorrespondentDto correspondent)
    {
        var corr = _mapper.Map<Correspondent>(correspondent);
        _context.Correspondents.Add(corr);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return corr;

    }

    public async Task<List<Correspondent>> GetAll()
    {
        List<Correspondent> corrs = null;  // Initialize to null or an empty list, depending on your preference.

        try
        {
            corrs = await _context.Correspondents.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
            // Handle the exception or log it as needed.
            // You might want to throw the exception again if you don't want to suppress it.
        }

        return corrs ?? new List<Correspondent>();  // Return the list or an empty list if an exception occurred.

    }

    public async Task<Correspondent> UpdateOne(long id, CorrespondentDto correspondent)
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

        await _context.SaveChangesAsync();

        return corr;    
        
    }

    public async Task<Correspondent> DeleteOne(long id)
    {
        Correspondent correspondent;

        try
        {
            correspondent = await _context.Correspondents.FindAsync(id);

            if (correspondent == null)
            {
                // Correspondent with the given id was not found
                return null;
            }

            _context.Correspondents.Remove(correspondent);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Handle the exception as needed
            throw; // rethrow the exception or handle it based on your requirements
        }

        return correspondent;
    }
}
