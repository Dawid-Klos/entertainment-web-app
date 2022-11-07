import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

import './Bookmark.scss';
import Card from "../Card/Card";
import Spinner from '../../UI/Spinner/Spinner';

const Bookmark = () => {

    const [loading, setLoading] = useState(true);
    const { content } = useOutletContext();

    useEffect(() => {
        if(content !== undefined) {
            setLoading(false)
        }

    }, [content]);
    
    if(loading) return <Spinner />;
    
    return(
        <>
            <div className="bookmark">
                <h1 className="bookmark__title">Bookmarked Movies</h1>
                <div className="bookmark__content">
                    {loading ? (<h2>Loading...</h2>) : (
                        content.map(movie => <Card key={movie.MovieId} movie={movie}/>)
                    )
                    }
                </div>
            </div>
            <div className="bookmark-tv">
                <h1 className="bookmark-tv__title">Bookmarked TV Series</h1>
                <div className="bookmark-tv__content">
                    {loading ? (<h2>Loading...</h2>) : (
                        content.map(movie => <Card key={movie.MovieId} movie={movie}/>)
                    )
                    }
                </div>
            </div>
        </>

    )
}

export default Bookmark;