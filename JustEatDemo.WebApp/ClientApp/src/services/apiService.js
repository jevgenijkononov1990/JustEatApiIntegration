import React from 'react';

const apiRequestTypes = {
    GET: "GET",
    POST: "POST",
    DELETE: "DELETE",
    PUT: "PUT",
}

const apiHttpCodes= {

    HTTP_200_OK : 200,
    HTTP_201_CREATED : 201,
    HTTP_202_ACCEPTED : 202,
    HTTP_400_BADREQUEST: 400,
    HTTP_401_UNAUTHORIZED: 401,
    HTTP_403_FORBIDDEN:403, 
    HTTP_500_INTERNALSERVERERROR: 500,
}

//Settings Shoukd be sourced from config file
const apiSettings = {
    statusCodes : apiHttpCodes,
    apiName: "api",
    postTypes: apiRequestTypes,
        apiEndpoints: {
            restaurant: "restaurant"
    }
}

const ApiService = {

     RestaurantGetRequest: function(requestObject) {
        try {

            var responseObj =
            {
                data: null,
                loading: true,
            };
        
            var requestUrl = apiSettings.apiName + '/' + apiSettings.apiEndpoints.restaurant;

            fetch(requestUrl,
                {
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    method: apiSettings.postTypes.POST,
                    body: JSON.stringify(requestObject)
                })
                //.then(response => response.json())
                .then(data => {

                    //responseObj.data = data;
                    responseObj.loading = false;
                    return responseObj;
                })
                .catch(error => {
                    console.log(error);
                    responseObj.loading = false;
                    return responseObj;
                });
        }
        catch (ex)
        {
            return null;
        }
    }

};
export default ApiService;
