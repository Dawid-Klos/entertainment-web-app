import { Route, Routes } from 'react-router-dom';
import './custom.scss';
import Layout from "./components/Layout";
import Home from "./components/Home/Home";

const App = () => {
    return (
        <>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                </Route>
            </Routes>
        </>
    )
}
  
export default App;