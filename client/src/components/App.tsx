import {createBrowserRouter, RouterProvider} from "react-router";
import Home from "@components/Home.tsx";
import {DevTools} from "jotai-devtools";
import 'jotai-devtools/styles.css'
import {Toaster} from "react-hot-toast"
import {finalUrl, playerClient} from "../core/baseUrl"
import {useEffect, useState} from "react";
import type {CreatePlayerDto, Player} from "@core/generated-ts-client.ts";


function App() {

    const [players, setPlayers] = useState<Player[]>([])
    const [myForm,setMyForm] = useState<CreatePlayerDto>({
        name: "",
        email: "",
    })

   /* useEffect(()=> {
        playerClient.getAllPlayers().then(r=> {
            setPlayers(r);
        })
    }, [])
*/
    useEffect(() => {
        playerClient.getAllPlayers().then(r => {
            console.log("Returned value:", r);
            setPlayers(r);
        });
    }, []);



    return (

        <div>

        <input value={myForm.name} onChange={e => setMyForm({...myForm, name: e.target.value})} placeholder="Name"/>
        <input value={myForm.email} onChange={e => setMyForm({...myForm, email: e.target.value})} placeholder="Email"/>
        <button onClick={() => {
            playerClient.createPlayer(myForm).then((result) => {
                console.log("Created player")
                setPlayers([...players, result])
            })
        }}>CREATE NEW USER</button>


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
