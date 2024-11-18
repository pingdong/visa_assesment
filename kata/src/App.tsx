import './App.css';
import ShoppingCart from './components/cart';
import PricingBoard from './components/pricing';
import ProductList from './components/product-list';
import { CartProvider } from './hooks/useCart';
import { ResultProvider } from './hooks/useResult';

function App() {
  return (

      <CartProvider>
        <ResultProvider>
          <div className="App">
            
            <div className="left">
              <ProductList />
            </div>

            <div className="right">

                <div className="right-top">
                  <ShoppingCart />
                </div>

                <div className="right-bottom">
                  <PricingBoard />
                </div>
              
            </div>
          </div>
        </ResultProvider>
      </CartProvider>
  );
}

export default App;
