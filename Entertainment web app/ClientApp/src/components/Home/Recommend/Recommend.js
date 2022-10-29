import "./Recommend.scss";
import Card from '../../Card/Card';

const Recommend = () => {

    return(
        <div className="recommend">
            <h1 className="recommend__title">Recommended for you</h1>
            <div className="recommend__content">
                <Card />
                <Card />
                <Card />
                <Card />
            </div>
        </div>
    )
}

export default Recommend;