﻿
using Microsoft.EntityFrameworkCore;

namespace Tarker.Booking.Application.DataBase.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IDeleteCustomerCommand
    {
        private readonly IDataBaseService _dataBaseService;

        public DeleteCustomerCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task<bool> Execute(int customerId)
        {
            var entity = await _dataBaseService.Customer.FirstOrDefaultAsync(p => p.CustomerId == customerId);
            if (entity == null) return false;
            _dataBaseService.Customer.Remove(entity);
            await _dataBaseService.SaveAsync();
            return true;
        }
    }
}
