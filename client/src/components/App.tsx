import {createBrowserRouter, RouterProvider} from "react-router";
import Home from "@components/Home.tsx";
import {DevTools} from "jotai-devtools";
import 'jotai-devtools/styles.css'
import {Toaster} from "react-hot-toast"
import {finalUrl, playerClient} from "../core/baseUrl"
import {useEffect, useState} from "react";
import type {Player} from "@core/generated-client.ts";


function App() {

    const [players, setPlayers] = useState<Player[]>([])

    useEffect(()=> {
        playerClient.getAllPlayers().then(r=> {
            setPlayers(r);
        })
    }, [])

    return (

        <div>

        <input placeholder="bingus"/>
        <input placeholder="getmeoutofhere"/>
        <button>CLIQUE MOI</button>


        <hr/>


            {
                players.map(t => {
                    return <div key={t.id}>

                        {JSON.stringify(t)}
                    </div>
                })
            }


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
        </div>
    )
}

export default App
