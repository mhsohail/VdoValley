using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VdoValley.Core.Models;
using VdoValley.Infrastructure.Repositories;

namespace VdoValley.Test
{
    [TestClass]
    public class AutoImportedVideosRepositoryTest
    {
        AutoImportedVideosRepository AutoImportedVideosRepo;

        [TestInitialize]
        public void TestSetup()
        {
            AutoImportedVideosRepo = new AutoImportedVideosRepository();
            AutoImportedVideosRepo.Add(new AutoImportedVideo { IsShared = false, URL = "url1" });
            AutoImportedVideosRepo.Add(new AutoImportedVideo { IsShared = false, URL = "url2" });
        }

        [TestMethod]
        public void IsAutoImportedVideosRepositoryInitalizeWithValidNumberOfData()
        {
            var result = AutoImportedVideosRepo.GetVideos();
            Assert.IsNotNull(result);
            var numberOfRecords = result.Count;
            Assert.AreEqual(2, numberOfRecords);
        }
    }
}
