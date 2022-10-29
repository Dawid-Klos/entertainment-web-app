import './Bookmark.scss';
import Card from "../Card/Card";

const Bookmark = () => {
    return(
        <>
            <div className="bookmark">
                <h1 className="bookmark__title">Bookmarked Movies</h1>
                <div className="bookmark__content">
                    <Card />
                    <Card />
                    <Card />
                    <Card />
                </div>
            </div>
            <div className="bookmark-tv">
                <h1 className="bookmark-tv__title">Bookmarked TV Series</h1>
                <div className="bookmark-tv__content">
                    <Card />
                    <Card />
                    <Card />
                    <Card />
                </div>
            </div>
        </>

    )
}

export default Bookmark;