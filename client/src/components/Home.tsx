import {Outlet, useNavigate} from "react-router";

export default function Home() {

    const navigate = useNavigate();

        return (
            <div style={{ padding: "2rem" }}>
                <h1>Welcome!</h1>
                <p>You are now logged in.</p>
            </div>
        );

}