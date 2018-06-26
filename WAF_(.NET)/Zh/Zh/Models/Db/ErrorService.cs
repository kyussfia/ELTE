using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.IO;
using SixLabors.ImageSharp;

namespace Zh.Models.Db
{
    public class ErrorService : IErrorInterface
    {
        private ErrorContext context;

        public ErrorService(ErrorContext cont)
        {
            context = cont;
        }

        public IQueryable<Error> GetLast30Errors()
        {
            return context.Errors.Where(e => DateTime.Now.Subtract(e.CreatedAt).Days <= 30).OrderByDescending(e => e.CreatedAt);
            //return GetValidItems().AsQueryable().OrderByDescending(i => i.CreatedAt).Take(20);
        }


        public Boolean IsErrorExist(int errId)
        {
            return context.Errors.Any(c => c.Id == errId);
        }

        public Error GetError(int errId)
        {
            return context.Errors.Single(i => i.Id == errId);
        }

        public IEnumerable<Error> GetErrors()
        {
            return context.Errors;
        }

        public Boolean createError(ViewModel.NewErrorViewModel model)
        {
            if (!this.validateViewModel(model))
            {
                return false;
            }

            var error = new Error
            {
                Description = model.Description,
                Title = model.Title,
                NumOfRequests = 1,
                CreatedAt = DateTime.Now
            };

            using (var memoryStream = new MemoryStream())
            {
                if (null != model.Picture && model.Picture.Length > 0)
                {
                    model.Picture.CopyTo(memoryStream);
                    var image = memoryStream.ToArray();
                    error.Picture = image;
                }
            }

            context.Errors.Add(error);

            return save();
        }

        protected Boolean save()
        {
            try
            {
                context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        protected Boolean validateViewModel(ViewModel.NewErrorViewModel viewModel)
        {
            if (viewModel == null)
            {
                return false;
            }

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(viewModel, new ValidationContext(viewModel, null, null), null))
            {
                return false;
            }

            return true;
        }

    }
}
