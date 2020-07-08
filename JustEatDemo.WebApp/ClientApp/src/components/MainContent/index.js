import React, { Component } from 'react';
import LoadingWrapper from '../Common/LoadingWrapper';
import RequestView from '../RequestView';
import RestaurantTable from '../Tables/RestaurantTable';
// import ApiService from '../../services/apiService.js';
import ValidationService from '../../services/validationService';


class MainContent extends Component {

    constructor(props) {
        super(props);
        this.state = {
            userName: null,
            userPassword: null,
            userPostCode: null,
            isLoadingData: false,
            requestData: null,
        }
    }

    restartSearch = () => {

        this.setState({ requestData: null,});
    }

    submitApiRequest = (userName, userPassword, userPostCode) => {

        var validationResult = this.ValidateInput(userName, userPassword, userPostCode);

        if (validationResult === false)
        {
            alert("Please check your input");
            return;
        }

        this.setState({
            isLoadingData: true,
            requestData: null,
            userName: userName,
            userPassword: userPassword,
            userPostCode: userPostCode
        });

        //var requestObject =
        //{
        //    username: userName,
        //    password: userPassword,
        //    postcode: userPostCode
        //};

        //var response = ApiService.RestaurantGetRequest(requestObject);

        //if (response === null || typeof (value) === 'undefined')
        //{
        //    this.setState({ isLoadingData: false, requestData: null });
        //    alert("Problem with Data");
        //}
        //else {
        //    this.setState({ isLoadingData: false, requestData: response.data });
        //}

        var requestUrl = "api" + '/' + "restaurant/" + `?username=${userName}&password=${userPassword}&postcode=${userPostCode}`;

        fetch(requestUrl,
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "GET",
            })
            .then(response => response.json())
            .then(response => {

                if (response.result !== null && response.result.restaurants !== null) {
                    this.setState({
                        isLoadingData: false,
                        requestData: response.result.restaurants
                    });
                }
                else {
                    this.setState({
                        isLoadingData: false,
                        requestData: null
                    });
                }
            })
            .catch(error => {
                console.log(error);
                alert("Problem with data");
                this.setState({ isLoadingData: false, requestData: null });
            });
    }

    ValidateInput(userName, userPassword, userPostCode) {

        if (!ValidationService.CheckValueForNullEmptyZeroLength(userName)) {
            return false;
        }
        if (!ValidationService.CheckValueForNullEmptyZeroLength(userPassword)) {
            return false;
        }
        if (!ValidationService.CheckValueForNullEmptyZeroLength(userPostCode)) {
            return false;
        }

        return true;
    }

    render() {
        return (
            <div>
                <div className="container">
                    {this.state.isLoadingData ?
                        <LoadingWrapper loaded={!this.state.isLoadingData} />
                        : (this.state.requestData !== null && this.state.requestData.length > 0 ) ?
                            // TableData
                            <RestaurantTable
                                tableData={this.state.requestData}
                                onClose={() => this.restartSearch()}
                            />
                            :
                                <RequestView onBtnRequestSubmit={(userName, userPassword, userPostCode) => this.submitApiRequest(userName, userPassword, userPostCode)} />
                     }
                </div>
            </div>
        );
    }
}
export default MainContent;