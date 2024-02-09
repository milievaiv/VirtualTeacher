// import node module libraries
import React, { Fragment, useContext, useEffect, useState } from 'react'
import { Alert, Form, Breadcrumb, Card, Col, Row, Table, Button, ListGroup } from 'react-bootstrap'
import { Link } from 'react-router-dom';

// import context file
import { CartContext } from 'context/Context';

// import required sub component
import CartItem from './CartItem';

// import required helper file
import { convertToCurrency } from 'helper/utils';

// import required hooks
import useCartOperations from 'hooks/useCartOperations';

const ShoppingCart = () => {
  const [cartSubTotal, setCartSubTotal] = useState(0);
  const [couponCode, setCouponCode] = useState('');
  const [couponCodeFound, setCouponCodeFound] = useState(null);
  const {
    getCartSubtotal,
    getDiscountAmount,
    getGrandTotal
  } = useCartOperations();

  const {
    CartState: { cartItems, cartSummary, couponCodes, totalQuantity },
    CartDispatch
  } = useContext(CartContext);

  useEffect(() => {
    setCartSubTotal(getCartSubtotal(cartItems));
    CartDispatch({
      type: 'UPDATE_QUANTITY',
      payload: cartItems.reduce((total, item) => total + item.quantity, 0)
    })
  }, [cartItems]);

  const applyCouponCode = () => {
    const couponInfo = couponCodes.find(coupon => coupon.code === couponCode);
    setCouponCodeFound(couponInfo)
    if (couponInfo) {
      CartDispatch({
        type: 'APPLY_COUPON',
        payload: {
          coupon: couponInfo.code,
          discount: couponInfo.discount
        }
      });
    }
    setCouponCode('');
  };

  const isCartEmpty = cartItems.length === 0;

  return (
    <Fragment>
      <Row>
        <Col lg={12} md={12} xs={12}>
          <div className="border-bottom pb-3 mb-3 ">
            <div className="mb-2 mb-lg-0">
              <h1 className="mb-0 h2 fw-bold">Shopping Cart </h1>
              <Breadcrumb>
                <Breadcrumb.Item to="#">Dashboard</Breadcrumb.Item>
                <Breadcrumb.Item to="#">Ecommerce</Breadcrumb.Item>
                <Breadcrumb.Item active>Shopping Cart</Breadcrumb.Item>
              </Breadcrumb>
            </div>
          </div>
        </Col>
      </Row>
      {isCartEmpty ?
        <Row>
          <Col>
            <Card>
              <Card.Header>
                <div className="d-flex ">
                  <h4 className="mb-0">Shopping Cart </h4>
                </div>
              </Card.Header>
              <Card.Body>
                <div>Opps !! You have no products in your shopping cart, start shopping now!</div>
              </Card.Body>
            </Card>
            <div className="mt-4 d-flex justify-content-between">
              <Link to="/dashboard/ecommerce/products/product-grid" className="btn btn-outline-primary">Continue Shopping</Link>
            </div>
          </Col>
        </Row>
        :
        <Row>
          <Col xs={12} className="mb-2">
            <Alert variant="warning">
              Use coupon code <strong>(GKDIS5 / GKDIS10 / GKDIS15)</strong> and get discount !
            </Alert>
          </Col>
          <Col lg={8}>
            <Card>
              <Card.Header>
                <div className="d-flex ">
                  <h4 className="mb-0">Shopping Cart <span className="text-muted ">({totalQuantity} Items)</span> </h4>
                </div>
              </Card.Header>
              <Card.Body>
                <div className="table-responsive table-card">
                  <Table className="table mb-0 text-nowrap">
                    <thead className="table-light">
                      <tr>
                        <th>Product</th>
                        <th>Price</th>
                        <th>Qty</th>
                        <th>Total</th>
                      </tr>
                    </thead>
                    <tbody>
                      {cartItems.map((product, index) => {
                        return (
                          <CartItem key={index} product={product} />
                        )
                      })}
                      <tr>
                        <td className="align-middle border-top-0 border-bottom-0 ">
                        </td>
                        <td className="align-middle border-top-0 border-bottom-0 ">
                          <h4 className="mb-0">Total</h4>
                        </td>
                        <td className="align-middle border-top-0 border-bottom-0 text-center ">
                          <span className="fs-4">{totalQuantity} (items)</span>
                        </td>
                        <td>
                          <h4 className="mb-0">{convertToCurrency(cartSubTotal)}</h4>
                        </td>
                      </tr>
                    </tbody>
                  </Table>
                </div>
              </Card.Body>
            </Card>
            <div className="mt-4 d-flex justify-content-between">
              <Link to="/dashboard/ecommerce/products/product-grid" className="btn btn-outline-primary">Continue Shopping</Link>
              {!isCartEmpty && <Link to="/dashboard/ecommerce/checkout" className="btn btn-primary">Checkout</Link>}
            </div>
          </Col>
          <Col lg={4}>
            <Card className="mb-4 mt-4 mt-lg-0">
              <Card.Body>
                <h4 className="mb-3">Have a promo code ?</h4>
                <Row className="g-3">
                  <Col>
                    <Form.Control
                      type="text"
                      placeholder="Enter promo code here"
                      autoComplete="off"
                      value={couponCode}
                      onChange={e => setCouponCode(e.target.value)}
                    />
                  </Col>
                  <Col xs="auto">
                    <Button
                      variant="dark"
                      onClick={applyCouponCode}
                    >
                      Apply
                    </Button>
                  </Col>
                </Row>
                <div className='mt-2'>
                  {couponCodeFound === null ? '' : couponCodeFound ?
                    <Alert variant="success">
                      <strong>{cartSummary.coupon}</strong> Coupon code is applied successfully!!.
                    </Alert>
                    : <Alert variant="danger">
                      Either <strong>{cartSummary.coupon}</strong> coupon code is invalid or expired.
                    </Alert>}
                </div>
              </Card.Body>
            </Card>
            <Card className="mb-4">
              <Card.Body>
                <h4 className="mb-3">Order Summary</h4>
                <ListGroup variant="flush">
                  <ListGroup.Item className='px-0 d-flex justify-content-between fs-5 text-dark fw-medium'>
                    <span>Sub Total :</span>
                    <span>{convertToCurrency(cartSubTotal)}</span>
                  </ListGroup.Item>
                  {cartSummary.coupon && <ListGroup.Item className='px-0 d-flex justify-content-between fs-5 text-dark fw-medium'>
                    <span>Discount <span className="text-muted">({cartSummary.coupon})</span>: </span>
                    <span>-{getDiscountAmount(cartItems)}</span>
                  </ListGroup.Item>}
                  <ListGroup.Item className='px-0 d-flex justify-content-between fs-5 text-dark fw-medium'>
                    <span>Shipping Charge :</span>
                    <span>{convertToCurrency(cartSummary.shipping)}</span>
                  </ListGroup.Item>
                  <ListGroup.Item className='px-0 d-flex justify-content-between fs-5 text-dark fw-medium'>
                    <span>Tax Vat {cartSummary.tax.toFixed(2)}% (included) :</span>
                    <span>{convertToCurrency(cartSummary.taxAmount)}</span>
                  </ListGroup.Item>
                </ListGroup>

              </Card.Body>
              <Card.Footer>
                <div className=" px-0 d-flex justify-content-between fs-5 text-dark fw-semibold">
                  <span>Total (USD)</span>
                  <span>{getGrandTotal(cartItems)}</span>
                </div>
              </Card.Footer>
            </Card>
          </Col>
        </Row>
      }
    </Fragment>
  )
}

export default ShoppingCart