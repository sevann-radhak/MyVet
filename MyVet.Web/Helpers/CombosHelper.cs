using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            var list = _dataContext.PetTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a pet type...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboServiceTypes()
        {
            var list = _dataContext.ServiceTypes.Select(st => new SelectListItem
            {
                Text = st.Name,
                Value = $"{st.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a service type...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboOwners()
        {
            var list = _dataContext.Owners
                .Select(o => new SelectListItem
                {
                    Text = o.User.FullNameWithDocument,
                    Value = $"{o.Id}"
                })
                .OrderBy(o => o.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select an owner...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPets(int ownerId)
        {
            var list = _dataContext.Pets.Where(p => p.Owner.Id == ownerId)
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = $"{p.Id}"
                })
                .OrderBy(o => o.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select the pet...]",
                Value = "0"
            });

            return list;
        }

    }
}
