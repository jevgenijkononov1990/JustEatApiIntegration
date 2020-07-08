using JustEatDemo.Common.Integrations.JustEat;
using JustEatDemo.WebApp.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JustEatDemo.Tests.ServiceTests.Repos
{
    public class JustEatSettingsRepositoryTests
    {
        public Mock<IOptions<JustEatIntegrationSettings>> _mockOptions;

        public JustEatSettingsRepository _justEatSettingsRepository;

        public JustEatSettingsRepositoryTests()
        {
            _mockOptions = new Mock<IOptions<JustEatIntegrationSettings>>();

            _justEatSettingsRepository = new JustEatSettingsRepository(_mockOptions.Object);
        }

        [Fact]
        public void Test_JustEatSettingsRepository_Constructor_ForDefense_When_Dependency_Null_Result_ThrowException()
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {

                new JustEatSettingsRepository(null);

            });

            //Arrange
            Assert.Throws<ArgumentNullException>(arrange);
        }

        [Fact]
        public void Test_JustEatSettingsRepository_GetSettings_When_OptionsNullOrValueNull_ResultNull()
        {
            //Arrange

            //Act
            JustEatIntegrationSettings result = _justEatSettingsRepository.GetSettings();
           
            //Arrange
            Assert.Null(result);
        }
    }
}
