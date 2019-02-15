using Library.Models.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _asset;

        public CatalogController(ILibraryAsset asset)
        {
            _asset = asset;
        }

        public IActionResult Index()
        {
            var assetModels = _asset.GetAll();

            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    Title = result.Title,
                    ImageUrl = result.ImageUrl,
                    Type = _asset.GetType(result.Id),
                    AuthorOrDirector = _asset.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _asset.GetDeweyIndex(result.Id),

                });

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
        }

    }
}
