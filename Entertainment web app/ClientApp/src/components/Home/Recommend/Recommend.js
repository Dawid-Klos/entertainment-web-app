import "./Recommend.scss";
import Card from '../../Card/Card';
import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

const Recommend = () => {

    const [loading, setLoading] = useState(true);
    const { content } = useOutletContext();

    useEffect(() => {
        if(content !== undefined) {
            setLoading(false)
        }

    }, [content]);
    
    return(
        <div className="recommend">
            <h1 className="recommend__title">Recommended for you</h1>
            <div className="recommend__content">
                {loading ? (<h2>Loading...</h2>) : (
                    content.map(movie => <Card key={movie.movieId} movie={movie}/>)
                )
                }
            </div>
        </div>
    )
}

export default Recommend;