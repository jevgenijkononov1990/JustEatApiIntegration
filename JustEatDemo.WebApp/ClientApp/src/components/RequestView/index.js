import React, { Component } from 'react';
import { PropTypes } from 'prop-types';

class RequestView extends Component {
    constructor(props) {
        super(props);
        this.state = {
            userName: null,
            userPassword: null,
            userPostCode: null
        }
    }
    static propTypes = {
        onBtnRequestSubmit: PropTypes.func.isRequired
    };

    static defaultProps = {

    };

    onUserNameValueChange = (e) => {
        this.setState({ userName: e.target.value });
    }

    onUserPasswordValueChange = (e) => {
        this.setState({ userPassword: e.target.value });
    }

    onUserPostCodeValueChange = (e) => {
        this.setState({ userPostCode: e.target.value });
    }


    render() {
        const { onBtnRequestSubmit }  = this.props;
        return (
            <div>
                    <div className="row">
                        <div className="CenterContainer">
                            <label
                                className="CenterContainer_label">
                                <b>Username</b>
                                <input
                                    type="text"
                                    onChange={e => this.onUserNameValueChange(e)}
                                    placeholder="Enter Username"
                                />
                            </label>
                        </div>
                    </div>
                    <div className="row">
                        <div className="CenterContainer">
                            <label
                                className="CenterContainer_label">
                                <b>Password</b>
                                <input
                                    type="password"
                                    onChange={e => this.onUserPasswordValueChange(e)}
                                    placeholder="Enter User Password"
                                />
                            </label>
                        </div>
                    </div>
                    <div className="row">
                        <div className="CenterContainer">
                            <label
                                className="CenterContainer_label">
                                <b>Post Code</b>
                                <input
                                    type="text"
                                    onChange={e => this.onUserPostCodeValueChange(e)}
                                    placeholder="Enter Postcode"
                                />
                            </label>
                        </div>
                    </div>
                    <div className="row">
                        <div className="CenterContainer">
                            <button
                                type="submit"
                                onClick={() => onBtnRequestSubmit(this.state.userName, this.state.userPassword, this.state.userPostCode)}
                            >Start Request</button>
                        </div>
                    </div>
                </div>
        );
    }
}
export default RequestView;