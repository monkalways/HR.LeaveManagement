using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext dbContext;

    public LeaveAllocationRepository(LeaveManagementDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<LeaveAllocation>> GetAllWithDetailsAsync()
    {
        var leaveAllocations = await dbContext.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetWithDetailsAsync(int id)
    {
        var leaveAllocation = await dbContext.LeaveAllocations
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
        return leaveAllocation;
    }
}
