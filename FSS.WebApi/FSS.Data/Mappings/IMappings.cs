using FSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSS.Data.Mappings
{
    public interface IMappings
    {
        ApplicationUser FromRegisterViewModelToApplicationUser(RegisterViewModel rvm);
    }
}
