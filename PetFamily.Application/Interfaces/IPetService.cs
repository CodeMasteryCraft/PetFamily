using LanguageExt.Common;
using PetFamily.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Interfaces
{
    public interface IPetService
    {
        /// <summary>
        /// Create a new Pet object
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<Guid>> CreateAsync(CreatePetModel model,CancellationToken cancellationToken);
    }
}
