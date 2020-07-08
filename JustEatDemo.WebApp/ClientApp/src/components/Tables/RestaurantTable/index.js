import React, { Component } from 'react';
import { PropTypes } from 'prop-types';


class RestaurantTable extends Component {
    static propTypes = {
        onClose: PropTypes.func.isRequired,
        tableData: PropTypes.arrayOf(PropTypes.shape()),
    };

    static defaultProps = {
        tableData: null
    }

    render() {
        const {
            tableData,
            onClose
        } = this.props;
        return (
            <div className="row">
                <div className="CenterContainer">
                    <div>
                        <a href="#" className="close" onClick={() => onClose()}></a> 
                    </div>

                    <div>
                        <table >
                            <thead>
                                <tr>
                                    <th align="center">Image</th>
                                    <th align="center">Name</th>
                                    <th align="center">Rating</th>
                                    <th align="center">Cuisine</th>

                                </tr>
                            </thead>
                            <tbody>
                                {(tableData != null && tableData.length > 0)
                                
                                    && tableData.map(item => item != null && <tr style={{ padding: '10px', backgroundColor: "#dfeff7" }} key={item.id}>
                                        <td align="center">{item.imageUrl != null ? <img src={item.imageUrl}/> : 'N/A'}</td>
                                        <td align="center">{item.name!= null ? item.name: 'N/A'}</td>
                                        <td align="center">{item.rating != null ? item.rating : 'N/A'}</td>
                                        <td align="center">{item.cuisineTypes != null ?
                                            <td>
                                                <table>
                                                    <tbody>
                                                            {item.cuisineTypes.map(value => (<tr>{value}</tr>))}
                                                    </tbody>
                                                </table>
                                            </td>
                                            : 'N/A'}
                                        </td>
                                    </tr>)
                                }
                                {(!tableData || tableData.length === 0)
                                    && <tr><td colSpan="7" style={{ textAlign: 'center' }}><p><b>No Data to Show</b></p></td></tr>
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        );
    }
}
export default RestaurantTable;