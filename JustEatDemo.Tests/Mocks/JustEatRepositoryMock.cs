using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat;
using Moq;
using System;

namespace JustEatDemo.Tests.Mocks
{
    public class JustEatRepositoryMock : Mock<IJustEatRepository>
    {
        public JustEatRepositoryMock Mock_GetSettings_CustomReturn(JustEatIntegrationSettings responseObject)
        {
            Setup(x => x.GetSettings())
                .Returns(responseObject);
            return this;
        }

        public JustEatRepositoryMock Mock_GetSettings_ThrowException()
        {
            Setup(x => x.GetSettings())
                .Throws(new Exception("Exception"));
            return this;
        }
    }
}
