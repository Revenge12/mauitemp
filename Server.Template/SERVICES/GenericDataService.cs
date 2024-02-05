using Microsoft.EntityFrameworkCore.Metadata.Internal;
using $safeprojectname$.Data;
using Shared.Template;

namespace $safeprojectname$.Services
{
    public class GenericDataService<T> where T : class
    {
        private readonly MainDbContext _context;

        public GenericDataService(MainDbContext context)
        {
            _context = context;
        }

        private T Value { get; set; }
        private ServiceResponse<T> Response { get; set; } = new ServiceResponse<T>();

        public async Task<ServiceResponse<T>> GenericUpdateInsert(T input)
        {
            try
            {
                await GetRow(input);

                await UpdateInsert(input);

                return Response;
            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;

                return Response;
            }




        }

        public async Task<ServiceResponse<T>> GenericRemove(T input)
        {
            try
            {
                await GetRow(input);

                if (Value == null)
                {
                    Response.Message = "Row could not be deleted";
                    Response.Success = true;
                    return Response;
                }

                await RemoveRow(input);

                return Response;
            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;

                return Response;

            }
            
        }

        private async Task GetRow(T input)
        {
            var entity = _context.Set<T>();

            var primarykey = await GetPrimaryKey(input);
            var prop = typeof(T).GetProperty(primarykey);

            var value = prop.GetValue(input, null);

            Value = await entity.FindAsync(value);
        }

        private async Task UpdateInsert(T? input)
        {
            _context.ChangeTracker.Clear();

            if(Value == null)
            {
                await _context.AddRangeAsync(input);
                Response.Success = true;
                Response.Message = "New recored inserted";
                Response.Data = input;
            }
            else
            {
                _context.UpdateRange(input);

                Response.Success = true;
                Response.Message = "Recored Updated";
                Response.Data = input;
            }

            await _context.SaveChangesAsync();
        }

        private async Task RemoveRow(T? input)
        {
            _context.RemoveRange(Value);



            await _context.SaveChangesAsync();

            Response.Data = Value;
            Response.Success = true;
            Response.Message = "Row deleted";
            
        }

        private async Task<string?> GetPrimaryKey<T>(T table)
        {
            var keyName = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();

            if (keyName == null)
            {
                return string.Empty;
            }
            else
            {
                return keyName;
            }
        }

    }
}
