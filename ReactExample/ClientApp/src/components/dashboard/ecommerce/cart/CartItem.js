// import node module libraries
import React, { Fragment, useContext } from 'react';
import { Image } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import CartQuantity from './CartQuantity';

// import context file
import { CartContext } from 'context/Context';

// import data files
import ProductsData from 'data/dashboard/ecommerce/ProductsData';

// import required helper file
import { convertToCurrency } from 'helper/utils';

const CartItem = ({ product }) => {
  const { id, quantity, totalPrice } = product;
  const { CartState, CartDispatch } = useContext(CartContext);

  const isFoundInCart = id => !!CartState.cartItems.find(cartItem => cartItem.id === id);
  const productInfo = ProductsData.filter((product) => product.id === id);
  const handleAddToCart = (quantity, add) => {
    if (isFoundInCart(product.id)) {
      const cartProducts = CartState.cartItems.find(item => item.id === product.id);
      CartDispatch({
        type: 'UPDATE_CART_ITEM',
        payload: {
          product: {
            ...cartProducts,
            quantity: add ? cartProducts.quantity + quantity : quantity,
            totalPrice: quantity * product.price
          },
          quantity
        }
      });
    } else {
      CartDispatch({
        type: 'ADD_TO_CART',
        payload: {
          product: {
            ...product,
            quantity,
            totalPrice: quantity * product.price
          }
        }
      });
    }
  };

  const handleRemove = (id) => {
    CartDispatch({
      type: 'REMOVE_CART_ITEM',
      payload: { id }
    });
  };

  const handleIncrease = () => {
    handleAddToCart(parseInt(quantity + 1));
  };

  const handleDecrease = () => {
    if (quantity > 1) {
      handleAddToCart(parseInt(quantity - 1));
    }
  };

  const handleChange = e => {
    handleAddToCart(parseInt(e.target.value < 1 ? 1 : e.target.value));
  };

  return (
    <Fragment>
      <tr >
        <td>
          <div className="d-flex">
            <div>
              <Image src={productInfo[0].images[0].image} alt="" className="img-4by3-md rounded" />
            </div>
            <div className="ms-4 mt-2 mt-lg-0">
              <h4 className="mb-1 text-primary-hover">
                {productInfo[0].name}
              </h4>
              <div>
                <span>Color: <span className="text-dark fw-medium">Orange</span></span> ,
                <span>Size: <span className="text-dark fw-medium"> 10</span></span>
              </div>
              <div className="mt-4">
                <Link to="#" className="text-body">Edit</Link>
                <Link to="#" className="text-body ms-3">Move to Wishlist</Link>
                <Link to="#" className="text-body ms-3" onClick={() => handleRemove(id)}>Remove</Link>
              </div>
            </div>
          </div>
        </td>
        <td>
          <h4 className="mb-0">{convertToCurrency(product.price)}</h4>
        </td>
        <td>
          <CartQuantity quantity={product.quantity}
            handleChange={handleChange}
            handleIncrease={handleIncrease}
            handleDecrease={handleDecrease} />
        </td>
        <td>
          <h4 className="mb-0">{convertToCurrency(totalPrice)}</h4>
        </td>
      </tr>
    </Fragment>
  );
};

export default CartItem;
