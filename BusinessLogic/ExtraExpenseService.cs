﻿using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Contracts;
using DataInterface;
using Models;
using Models.ViewModels;

namespace BusinessLogic
{
    public class ExtraExpenseService : FormService<IExtraExpenseRepository, ExtraExpense, ExtraExpenseViewModel>, IExtraExpenseService
    {
        private IExtraExpenseRepository ExtraExpenseRepository { get; set; }

        public ExtraExpenseService(IExtraExpenseRepository repository) : base(repository)
        {
            ExtraExpenseRepository = repository;
        }

        public ExtraExpense GetByChildId(long childId)
        {
            return ExtraExpenseRepository.GetChildById(childId);
        }

        public List<ExtraExpense> GetAllByUserId(long userId)
        {
            return ExtraExpenseRepository.GetAllByUserId(userId).ToList();
        }
    }
}