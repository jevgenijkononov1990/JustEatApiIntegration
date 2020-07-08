using JustEatDemo.Common.Integrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;


namespace JustEatDemo.Tests.EnumsTests
{
    public class HttpApiMethodsTypeParserTests
    {
        [Theory]
        [InlineData("GET")]
        [InlineData("POST")]
        [InlineData("PUT")]
        [InlineData("DELETE")]
        [InlineData("PATCH")]
        public void Test_HttpApiMethodsTypeConverter_StringToEnum_SuccessFullParse(string methodName)
        {
            //Arrange

            //Act
            var result = HttpApiMethodsTypeConverter.StringToEnum(methodName);


            //Arrange
            Assert.NotNull(result);
            Assert.IsType<HttpApiMethodsType>(result);
        }

        [Theory]
        [InlineData("blb")]
        [InlineData(null)]
        [InlineData("")]
        public void Test_HttpApiMethodsTypeConverter_StringToEnum_ErrorInParser(string methodName)
        {
            //Arrange
            Action arrange;

            //Act
            arrange = new Action(() =>
            {
                HttpApiMethodsTypeConverter.StringToEnum(methodName);
            });

            //Arrange
            Assert.Throws<InvalidEnumArgumentException>(arrange);
        }

        [Theory]
        [InlineData(HttpApiMethodsType.GET)]
        [InlineData(HttpApiMethodsType.POST)]
        [InlineData(HttpApiMethodsType.PUT)]
        [InlineData(HttpApiMethodsType.DELETE)]
        [InlineData(HttpApiMethodsType.PATCH)]
        public void Test_HttpApiMethodsTypeConverter_EnumToString_SuccessFullParse(HttpApiMethodsType methodName)
        {
            //Arrange

            //Act
            var result = HttpApiMethodsTypeConverter.EnumToString(methodName);


            //Arrange
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(300)]
        public void Test_HttpApiMethodsTypeConverter_EnumToString_ErrorInParser(int methodName)
        {
            //Arrange
            Action arrange;
            var method = (HttpApiMethodsType)(methodName);

            //Act
            arrange = new Action(() =>
            {
                HttpApiMethodsTypeConverter.EnumToString(method);
            });

            //Arrange
            Assert.Throws<InvalidEnumArgumentException>(arrange);
        }
    }
}
