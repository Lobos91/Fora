namespace Fora.UI.Data
{
    public class InterestManager
    {
        private readonly AppDbContextUI _context;
        public InterestManager(AppDbContextUI context)
        {
            _context = context;
        }


        public async Task CreateInterestAsync(InterestModel newInterest)
        {
            _context.Interests.Add(newInterest);
            await _context.SaveChangesAsync();
        }
    }
}
