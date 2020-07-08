import React, { Component } from 'react';
import MainContent from '../../components/MainContent';
import Footer from '../../components/Footer';
import Header from '../../components/Header';

export default class Home extends Component {
  render() {
      return (
          <div>
              <Header/>
              <MainContent/>
              <Footer/>
          </div>
    );
  }
}
