import { useState } from "react";
import { playerClient } from "../core/baseUrl";

type LoginRequest = {
    email: string;
    password: string;
};

export default function Login() {
    const [form, setForm] = useState<LoginRequest>({
        email: "",
        password: "",
    });

    const [error, setError] = useState<string | null>(null);

    const handleLogin = async () => {
        try {
            setError(null);

            const result = await playerClient.login(form);

            console.log("Logged in:", result);
            // later:
            // localStorage.setItem("token", result.token);
            // navigate("/");
        } catch (err) {
            setError("Invalid email or password");
        }
    };

    return (
        <div style={{ maxWidth: 400, margin: "2rem auto" }}>
            <h2>PLEASE LOG INTO THE THINGY</h2>

            <input
                type="email"
                placeholder="Email"
                value={form.email}
                onChange={(e) =>
                    setForm({ ...form, email: e.target.value })
                }
            />

            <input
                type="password"
                placeholder="Password"
                value={form.password}
                onChange={(e) =>
                    setForm({ ...form, password: e.target.value })
                }
            />

            <button onClick={handleLogin}>
                Login
            </button>

            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}
