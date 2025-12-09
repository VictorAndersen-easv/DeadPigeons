import {createBrowserRouter, RouterProvider} from "react-router";
import Home from "@components/Home.tsx";
import {DevTools} from "jotai-devtools";
import 'jotai-devtools/styles.css'
import {Toaster} from "react-hot-toast"
import {finalUrl} from "../core/baseUrl"
import {useEffect} from "react";


function App() {

    useEffect(()=> {

    })

    return (
        <>
            <h1>Look at me! Im a webpage!</h1>

            <div>
            <button onClick={()=> {
                fetch(finalUrl)
                    .then(response => {
                        console.log(response)
                    }).catch(e => {
                        console.log(e)
                        })
            }}> CLICK ME</button>
            </div>

            <DevTools/>
            <Toaster
                position="top-center"
                reverseOrder={false}
            />
        </>
    )
}

export default App
