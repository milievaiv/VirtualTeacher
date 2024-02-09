// import node module libraries
import React from 'react'
import { Col, Row } from 'react-bootstrap'

// import custom components
import GKLightbox from 'components/elements/lightbox/GKLightbox'

// import media files
import ProductImage1 from 'assets/images/ecommerce/product-slide-1.jpg';
import ProductImage2 from 'assets/images/ecommerce/product-slide-2.jpg';
import ProductImage3 from 'assets/images/ecommerce/product-slide-3.jpg';
import ProductImage4 from 'assets/images/ecommerce/product-slide-4.jpg';
import ProductImage5 from 'assets/images/ecommerce/product-slide-5.jpg';

const ProductGallery = () => {
    return (
        <Row>
            <Col xs={12}>
                <div className="mb-4">
                    <GKLightbox image={ProductImage1} />
                </div>
            </Col>
            <Col lg={6} xs={12}>
                <div className="mb-4"> <GKLightbox image={ProductImage2} /></div>
            </Col>
            <Col lg={6} xs={12}>
                <div className="mb-4"> <GKLightbox image={ProductImage3} /> </div>
            </Col>
            <Col lg={6} xs={12}>
                <div className="mb-4"> <GKLightbox image={ProductImage4} /></div>
            </Col>
            <Col lg={6} xs={12}>
                <div className="mb-4"> <GKLightbox image={ProductImage5} /> </div>
            </Col>
        </Row>
    )
}

export default ProductGallery