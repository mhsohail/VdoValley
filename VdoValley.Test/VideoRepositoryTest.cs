using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VdoValley.Infrastructure;
using System.Data.Entity;

namespace VdoValley.Test
{
    [TestClass]
    public class VideoRepositoryTest
    {
        VideosRepository repo;

        [TestInitialize]
        public void TestSetup()
        {
            //DbInitializer db = new DbInitializer();
            //Database.SetInitializer(db);
            repo = new VideosRepository();
        }

        [TestMethod]
        public void IsRepositoryInitalizeWithValidNumberOfData()
        {
            var result = repo.GetVideos();
            Assert.IsNotNull(result);
            var numberOfRecords = result.Count;
            Assert.AreEqual(2, numberOfRecords);
        }
    }
}
