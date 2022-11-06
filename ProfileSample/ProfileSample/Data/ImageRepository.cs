using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Data
{
    public class ImageRepository : IDisposable
    {
        private readonly ProfileSampleEntities _dbContext;

        public ImageRepository()
        {
            _dbContext = new ProfileSampleEntities();
        }

        public List<ImageModel> GetFirstImageModels(int count)
        {
            var sources = _dbContext.ImgSources.Take(count);
            var images = sources
                .Select(s => new ImageModel()
                {
                    Name = s.Name,
                    Data = s.Data
                });

            return images.ToList();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}