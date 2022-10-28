import "./Trending.scss";

const Trending = () => {
    
    return(
        <div className="card">
            <span className="card__bookmark">
                <img src="./assets/thumbnails/1998/trending/large.jpg" alt="asd" />
            </span>
            <div className="card__info">
                <p className="card__info--year">2019</p>
                <p className="card__info--category">Movie</p>
                <p className="card__info--rating">PG</p>
            </div>
            <h2 className="card__title">Panie, działa!</h2>
            
        </div>
    )
}

export default Trending;