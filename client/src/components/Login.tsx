import { useState } from "react";
import { playerClient } from "../core/baseUrl";
import { useNavigate } from "react-router";


type LoginRequest = {
    email: string;
    password: string;
};

export default function Login() {
    const [form, setForm] = useState<LoginRequest>({
        email: "",
        password: "",
    });

    const navigate = useNavigate();


    const [error, setError] = useState<string | null>(null);

    const handleLogin = async () => {
        try {
            setError(null);

            const result = await playerClient.login(form);

            console.log("Logged in:", result);
            // later:
            //localStorage.setItem("token", result.token);
            navigate("/home");
        } catch (err) {
            setError("Invalid email or password");
        }
    };

    return (
        <div style={{ maxWidth: 400, margin: "2rem auto" }}>
            <h2>Please enter Email and Password.</h2>

            <input
                type="email"
                placeholder="Email"
                value={form.email}
                onChange={(e) =>
                    setForm({ ...form, email: e.target.value })
                }
                style={{
                    marginBottom: "1rem",
                    marginTop: "1rem",
            }}
            />

            <input
                type="password"
                placeholder="Password"
                value={form.password}
                onChange={(e) =>
                    setForm({ ...form, password: e.target.value })
                }
                style={{
                    marginBottom: "1rem",
                    marginTop: "1rem",
                }}
            />

            <button onClick={handleLogin} style={{
                border: "2px solid black",
                background: "white",
                padding: "0.5rem 1rem",
                cursor: "pointer",
                marginTop: "1rem",
            }}>
                Login
            </button>

            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}
